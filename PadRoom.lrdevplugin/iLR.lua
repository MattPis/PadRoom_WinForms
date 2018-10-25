
require 'Develop_Params.lua' 

local LrApplicationView   = import 'LrApplicationView'
local LrFunctionContext   = import 'LrFunctionContext'
local LrShell             = import 'LrShell'
local LrSocket            = import 'LrSocket'
local LrApplication       = import 'LrApplication'
local LrTasks             = import 'LrTasks'
local LrDevelopController = import 'LrDevelopController'
local LrDialogs           = import 'LrDialogs'
local LrSelection         = import 'LrSelection'
local LrUndo              = import 'LrUndo'
iLR = {PARAM_OBSERVER = {}, PICKUP_ENABLED = true, SERVER = {} } 

local applySettings
local sendSettings
local startServer
local updateParam
local lastKnownTempMin;

function applySettings(message)
    
  local _, _, name, value = string.find( message, '(%S+)%s(%S+)' )
    --  LrDialogs.message('Message', param)

  for _, param in ipairs(DEVELOP_PARAMS) do
    if(param == name) then
        LrDevelopController.setValue(param, (tonumber(value)))
    end
end

  if (name == 'reset') then
    LrDevelopController.resetToDefault(value)
  end
  
  if (name == 'nextPhoto') then
      LrSelection.nextPhoto()
  end
  if (name == 'previousPhoto') then
    LrSelection.previousPhoto()
  end
  if (name == 'resetAll') then
    LrDevelopController.resetAllDevelopAdjustments()
  end
  if(name == 'undo') then
    LrUndo.canUndo()
    LrUndo.undo()
  end 
  if(name == 'redo') then
    LrUndo.canRedo()
    LrUndo.redo()
  end 
  if(name == 'zoomOut') then
    LrApplicationView.zoomOut()
  end
  
  if(name == 'zoomIn') then
    LrApplicationView.zoomIn()
  end
  
  if(name == 'library' or name == 'develop') then
    LrApplicationView.switchToModule( name )
  end
  
  if(name == 'connected') then
    --LrTasks.sleep( 1/2 )
    sendAllSettings()
  end
  if (name == 'tool') then 
    LrDevelopController.selectTool(value)
    end
      --LrDialogs.message('Message', name)

end
function sendAllSettings()
  lastKnownTempMin = 0
  sendTempRange()
  sendVersionNumber()
  for _, param in ipairs(DEVELOP_PARAMS) do
  --  LrDialogs.message('Message', param)
  iLR.SERVER:send(string.format('%s %g \n', param, LrDevelopController.getValue(param)))  -- sends string followed by value
  
end
end

function sendSettings( observer )

  for _, param in ipairs(DEVELOP_PARAMS) do
    if(observer[param] ~= LrDevelopController.getValue(param)) then
      iLR.SERVER:send(string.format('%s %g \n', param, LrDevelopController.getValue(param)))  -- sends string followed by value
      observer[param] = LrDevelopController.getValue(param)
    end
    sendTempRange()
  end

end

function sendTempRange()
  
      local tempMin, tempMax = LrDevelopController.getRange( 'Temperature' )
      local tintMin, tintMax = LrDevelopController.getRange( 'Tint')
      if (lastKnownTempMin ~= tempMin) then
        lastKnownTempMin = tempMin
        iLR.SERVER:send(string.format('%s %g %g \n', 'TempRange', tempMin, tempMax))  -- sends string followed by value\
        iLR.SERVER:send(string.format('%s %g %g \n', 'TintRange', tintMin, tintMax))
      end
end
function sendVersionNumber()
  iLR.SERVER:send(string.format('%s %g \n', 'version', '1.4'))  -- sends string followed by value\
end

function startServer(context)
  iLR.SERVER = LrSocket.bind {
    functionContext = context,
    plugin = _PLUGIN,
    port = 58764,
    mode = 'send',
    onClosed = function( socket ) 
    end,
    onError = function( socket, err )
      socket:reconnect()
    end,
  }
end

-- Main activity
LrTasks.startAsyncTask( function()

    LrFunctionContext.callWithContext( 'socket_remote', function( context )
        LrDevelopController.revealAdjustedControls( true ) -- reveal affected parameter in panel track

        LrDevelopController.addAdjustmentChangeObserver( context, iLR.PARAM_OBSERVER, sendSettings )

        local client = LrSocket.bind {
          functionContext = context,
          plugin = _PLUGIN,
          port = 58763,
          mode = 'receive',
          onMessage = function(socket, message)
            applySettings(message)
          end,
          onClosed = function( socket )
            socket:reconnect()

            iLR.SERVER:close()
            startServer(context)
          end,
          onError = function(socket, err)
            if err == 'timeout' then 
              socket:reconnect()
            end
          end
        }

        startServer(context)

        while true do
          LrTasks.sleep( 1/2 )
        end

        client:close()
        iLR.SERVER:close()
      end )
  end )

LrTasks.startAsyncTask( function()
        LrDevelopController.revealAdjustedControls(false)
        LrApplicationView.switchToModule( 'develop' )
		 if(WIN_ENV) then
     -- LrShell.openFilesInApp({_PLUGIN.path..'/info.lua'}, _PLUGIN.path..'/PadRoom.exe') 
    else
     -- LrShell.openFilesInApp({_PLUGIN.path..'/info.lua'}, _PLUGIN.path..'/PadRoom.app') 
    end
    
  end)
