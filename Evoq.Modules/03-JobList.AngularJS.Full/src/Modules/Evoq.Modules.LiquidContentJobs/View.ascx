<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Evoq.Modules.LiquidContentJobs.View" %>
<div runat="server" ID="ScopeWrapper" class="job-list">
<div ng-controller="job-list-<%= ScopeWrapper.ClientID %>">
    <div id='ang-<%= ScopeWrapper.ClientID %>'></div>
</div>

<script>
    if (typeof window.dnn.moduleIds === "undefined") {
        window.dnn.moduleIds = [];
    }
    window.dnn.moduleIds.push("<%= ScopeWrapper.ClientID %>");
    window.dnn.app<%= ScopeWrapper.ClientID %> = () => {
        return {
            template: require('./app.html'),
            controller: 'job-list-' + moduleId,
            controllerAs: 'app'
        }
};

</script>
<script type="text/javascript" src="DesktopModules/LiquidContentJobs/Scripts/jobList.js"></script>
</div>
