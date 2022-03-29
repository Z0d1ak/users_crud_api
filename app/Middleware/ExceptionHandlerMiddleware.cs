using System;
using System.Text.Json;
using System.Threading.Tasks;
using app.Contracts.Dtos;
using app.Converters;
using app.Shared.Exceptions;
using Microsoft.AspNetCore.Http;

namespace app.Middleware
{
    public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (ExceptionWithCode exceptionWithCode)
			{
				var result = new ErrorResponseDto
				{
					Success = false,
					ErrorCode = exceptionWithCode.ErrorCode.ToDto(),
					ErrorMessage = exceptionWithCode.Message,
				};
				var response = JsonSerializer.Serialize(result);
				await context.Response.WriteAsync(response);
			}
            catch(Exception e)
            {
				var result = new ErrorResponseDto
				{
					Success = false,
					ErrorCode = ErrorCode.Unknown.ToDto(),
					ErrorMessage = "Unknown exception going to client",
				};
				var response = JsonSerializer.Serialize(result);
				await context.Response.WriteAsync(response);
			}
		}
	}
}
