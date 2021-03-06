﻿using System.Threading.Tasks;
using Ahk.GitHub.Monitor.EventHandlers;
using Ahk.GitHub.Monitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ahk.GitHub.Monitor.Tests.UnitTests
{
    [TestClass]
    public class EventDispatchServiceTest
    {
        [TestMethod]
        public async Task DispatcherCallsAllEventHandlers()
        {
            var services = new ServiceCollection();
            var config = new EventDispatchConfigBuilder(services)
                .Add<EventHandler1A>("event1")
                .Add<EventHandler1B>("event1")
                .Add<EventHandler2>("event2");

            var service = new EventDispatchService(services.BuildServiceProvider(), config);
            await service.Process("event1", "dummy request", new WebhookResult());

            Assert.IsTrue(EventHandler1A.Invoked);
            Assert.IsTrue(EventHandler1A.Invoked);
            Assert.IsFalse(EventHandler2.Invoked);
        }

        private class EventHandler1A : IGitHubEventHandler
        {
            public static bool Invoked { get; private set; }
            public Task<EventHandlerResult> Execute(string requestBody)
            {
                Invoked = true;
                return Task.FromResult(new EventHandlerResult(string.Empty));
            }
        }

        private class EventHandler1B : IGitHubEventHandler
        {
            public static bool Invoked { get; private set; }
            public Task<EventHandlerResult> Execute(string requestBody)
            {
                Invoked = true;
                return Task.FromResult(new EventHandlerResult(string.Empty));
            }
        }

        private class EventHandler2 : IGitHubEventHandler
        {
            public static bool Invoked { get; private set; }
            public Task<EventHandlerResult> Execute(string requestBody)
            {
                Invoked = true;
                return Task.FromResult(new EventHandlerResult(string.Empty));
            }
        }
    }
}
