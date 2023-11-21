using Microsoft.AspNetCore.Mvc.Filters;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class KafkaProducerAttribute : ActionFilterAttribute
{
    private readonly string _topicName;
    private readonly string _operationName;

    public KafkaProducerAttribute(string topicName, string operationName)
    {
        _topicName = topicName;
        _operationName = operationName;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var kafkaProducerService = context.HttpContext.RequestServices.GetRequiredService<IKafkaProducerService>();

        if (!string.IsNullOrEmpty(_topicName) && !string.IsNullOrEmpty(_operationName))
        {
            await kafkaProducerService.ProduceMessageAsync(_topicName, _operationName);
        }

        await next();
    }
}
