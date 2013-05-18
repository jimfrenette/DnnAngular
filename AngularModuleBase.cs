/*
' Copyright (c) 2013  DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Modules.Angular
{
    public class AngularModuleBase : PortalModuleBase
    {

        public static void SessionTimerJavaScript(PortalModuleBase portalModuleBase)
        {
            if (portalModuleBase.Page != null)
            {
                string timerScript = "<script src=\"/DesktopModules/Angular/Scripts/SessionTimer.js\" type=\"text/javascript\"></script>";

                int sessionWarningThresholdInMilliSeconds = int.Parse(portalModuleBase.Settings["SessionTimerThreshold"].ToString()) * 60000;
                int sessionDurationInMilliSeconds = portalModuleBase.Page.Session.Timeout * 60000;

                //guard against invalid warning threshold.
                if (sessionWarningThresholdInMilliSeconds >= sessionDurationInMilliSeconds)
                {
                    sessionWarningThresholdInMilliSeconds = sessionDurationInMilliSeconds - 1000;
                }

                string timerVars = string.Format("sessionApp.constant('settings',{{ threshold: {0}, sessionDuration: {1} }});\n", sessionWarningThresholdInMilliSeconds.ToString(), sessionDurationInMilliSeconds.ToString());

                if (!portalModuleBase.Page.ClientScript.IsStartupScriptRegistered("_SessionTimerInclude_"))
                {
                    portalModuleBase.Page.ClientScript.RegisterStartupScript(portalModuleBase.Page.GetType(), "_SessionTimerInclude_", timerScript, false);
                }
                if (!portalModuleBase.Page.ClientScript.IsStartupScriptRegistered("_SessionTimerVars_"))
                {
                    portalModuleBase.Page.ClientScript.RegisterStartupScript(portalModuleBase.Page.GetType(), "_SessionTimer_", timerVars, true);
                }
            }
        }

    }
}