using System;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Logging;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a class that handles the interaction with the proxy builder
    /// </summary>
    public class ProxiesHandler
    {
        readonly TemplateLoader _templateLoader;
        readonly DolittleArtifactTypes _artifactTypes;
        readonly ILogger _logger;

        /// <summary>
        /// Instantiates a new instance of <see cref="ProxiesHandler"/>
        /// </summary>
        /// <param name="templateLoader"></param>
        /// <param name="artifactTypes"></param>
        /// <param name="logger"></param>
        public ProxiesHandler(TemplateLoader templateLoader, DolittleArtifactTypes artifactTypes, ILogger logger)
        {
            _templateLoader = templateLoader;
            _artifactTypes = artifactTypes;
            _logger = logger;
        }

        /// <summary>
        /// Creates the proxies given a list of artifacts and configurations
        /// </summary>
        /// <param name="artifacts"></param>
        /// <param name="boundedContextConfiguration"></param>
        /// <param name="artifactsConfiguration"></param>
        public void CreateProxies(Type[] artifacts, BoundedContextConfiguration boundedContextConfiguration, ArtifactsConfiguration artifactsConfiguration)
        {
            var builder = new ProxiesBuilder(_templateLoader, artifacts, _artifactTypes, _logger);
            builder.GenerateProxies(artifactsConfiguration, boundedContextConfiguration);
        }
    }
}