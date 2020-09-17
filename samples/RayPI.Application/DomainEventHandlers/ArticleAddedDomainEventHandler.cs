﻿using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Extensions.Json;
using RayPI.Application.IntegrationEvents;
using RayPI.Application.IntegrationEvents.Events;
using RayPI.Domain.Events;

namespace RayPI.Application.DomainEventHandlers
{
    /// <summary>
    /// 文章新增事件处理器
    /// </summary>
    public class ArticleAddedDomainEventHandler : INotificationHandler<ArticleAddedDomainEvent>
    {
        private readonly ILogger<ArticleAddedDomainEventHandler> _logger;
        private readonly ICapPublisher _capPublisher;

        public ArticleAddedDomainEventHandler(ILogger<ArticleAddedDomainEventHandler> logger,
            ICapPublisher capPublisher)
        {
            _logger = logger;
            this._capPublisher = capPublisher;
        }

        public async Task Handle(ArticleAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            //暂时没想好要做啥，记个日志并且发送综合事件
            _logger.LogInformation($"进入领域事件处理器，新增了文章：{notification.Article.AsJsonStr()}");

            //await _integrationEventService.PublishEvent(new ArticleAddedIntegrationEvent(notification.Article));
            await _capPublisher.PublishAsync("ArticleAdded", new ArticleAddedIntegrationEvent(notification.Article, notification.Article.Id));
        }
    }
}
