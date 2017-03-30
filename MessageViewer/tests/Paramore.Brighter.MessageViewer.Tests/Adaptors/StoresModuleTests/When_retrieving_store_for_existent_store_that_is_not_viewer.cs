﻿using Nancy.Json;
using Nancy.Testing;
using NUnit.Framework;
using Paramore.Brighter.MessageViewer.Adaptors.API.Modules;
using Paramore.Brighter.MessageViewer.Adaptors.API.Resources;
using Paramore.Brighter.MessageViewer.Ports.ViewModelRetrievers;
using Paramore.Brighter.Viewer.Tests.TestDoubles;

namespace Paramore.Brighter.Viewer.Tests.Adaptors.StoresModuleTests
{
    [Ignore("Returns wrong error code")]
    [TestFixture]
    public class RetreiveMessageStoreNotInViewerTests
    {
        private Browser _browser;
        private BrowserResponse _result;
        private readonly string _storesUri = "/stores";

       [SetUp]
        public void Establish()
        {
            _browser = new Browser(new ConfigurableBootstrapper(with =>
            {
                ConfigureStoreModuleForStoreError(with, MessageStoreViewerModelError.StoreMessageViewerNotImplemented);
            }));
        }

        [Test]
        public void When_retrieving_store_for_existent_store_that_is_not_viewer()
        {
            _result = _browser.Get(_storesUri, with =>
                {
                    with.Header("accept", "application/json");
                    with.HttpRequest();
                })
                .Result;

            //should_return_404_NotFound
            Assert.AreEqual(Nancy.HttpStatusCode.NotFound, _result.StatusCode);
            //should_return_json
            StringAssert.Contains("application/json", _result.ContentType);
            //should_return_error_detail
            var serializer = new JavaScriptSerializer();
            var model = serializer.Deserialize<MessageViewerError>(_result.Body.AsString());

            Assert.NotNull(model);
            StringAssert.Contains("IMessageStoreViewer", model.Message);
        }

        private void ConfigureStoreModuleForStoreError(ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator with, MessageStoreViewerModelError messageStoreViewerModelError)
        {
            var listViewRetriever = FakeActivationListModelRetriever.Empty();
            var storeRetriever = new FakeMessageStoreViewerModelRetriever(messageStoreViewerModelError);
            var messageRetriever = FakeMessageListViewModelRetriever.Empty();

            with.Module(new StoresNancyModule(listViewRetriever, storeRetriever, messageRetriever));
        }
    }
}