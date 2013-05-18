<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="DotNetNuke.Modules.Angular.View" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnJsInclude ID="DnnAngularSessionInclude" runat="server" FilePath="~/DesktopModules/Angular/Scripts/SessionTimer.js" ForceProvider="DnnFormBottomProvider" />
<!-- In production, use minified version (angular.min.js) -->
<dnn:DnnJsInclude ID="DnnAngularInclude" runat="server" FilePath="~/DesktopModules/Angular/Library/Angular/angular.js" ForceProvider="DnnFormBottomProvider" />

<div ng-app="sessionApp" ng-controller="SessionCtrl">
    <div id="dialog-session" title="Session Expiration">
        <p>Your session will expire in:</p>
        <p>{{ remainingTime | minute }} minutes.</p>
        </div>
    </div>
</div>
