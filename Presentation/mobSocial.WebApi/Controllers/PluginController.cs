﻿using System.Web.Http;
using mobSocial.Services.Plugins;
using mobSocial.WebApi.Configuration.Mvc;
using mobSocial.WebApi.Models.Plugin;

namespace mobSocial.WebApi.Controllers
{
    [RoutePrefix("plugins")]
    public class PluginController : RootApiController
    {
        private readonly IPluginInstallerService _pluginInstallerService;
        private readonly IPluginFinderService _pluginFinderService;

        public PluginController(IPluginInstallerService pluginInstallerService, 
            IPluginFinderService pluginFinderService)
        {
            _pluginInstallerService = pluginInstallerService;
            _pluginFinderService = pluginFinderService;
        }

        [HttpPost]
        [Route("install")]
        public IHttpActionResult Install(PluginInfoModel model)
        {
            //first find the plugin
            var pluginInfo = _pluginFinderService.FindPlugin(model.SystemName);
            if (pluginInfo == null)
            {
                //was it a correct plugin?
                VerboseReporter.ReportError("The plugin doesn't exist", "plugin");
                return RespondFailure();
            }

            //install the plugin
            _pluginInstallerService.Install(pluginInfo);

            VerboseReporter.ReportSuccess("The plugin has been installed", "plugin");
            return RespondSuccess();
        }

        [HttpPost]
        [Route("uninstall")]
        public IHttpActionResult Uninstall(PluginInfoModel model)
        {
            //first find the plugin
            var pluginInfo = _pluginFinderService.FindPlugin(model.SystemName);
            if (pluginInfo == null)
            {
                //was it a correct plugin?
                VerboseReporter.ReportError("The plugin doesn't exist", "plugin");
                return RespondFailure();
            }

            if (pluginInfo.IsSystemPlugin)
            {
                //was it a correct plugin?
                VerboseReporter.ReportError("The plugin can't be uninstalled", "plugin");
                return RespondFailure();
            }
            //uninstall the plugin
            _pluginInstallerService.Uninstall(pluginInfo);

            VerboseReporter.ReportSuccess("The plugin has been uninstalled", "plugin");
            return RespondSuccess();
        }
    }
}