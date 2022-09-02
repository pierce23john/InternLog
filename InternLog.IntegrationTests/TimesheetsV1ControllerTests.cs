using DateOnlyTimeOnly.AspNet.Converters;
using FluentAssertions;
using InternLog.Api.Features.V1;
using InternLog.Api.Features.V1.Timesheets.CreateTimesheet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace InternLog.IntegrationTests
{
	public class TimesheetsV1ControllerTests : IntegrationTest
	{
		[Fact]
		public async Task GetTimesheets_WithoutAny_ReturnsEmptyResponse()
		{
			// Arrange
			await AuthenticateAsync();

			// Act
			var response = await HttpClient.GetAsync(ApiV1Routes.Timesheets.GetAll);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			(await response.Content.ReadFromJsonAsync<List<CreateTimesheetResponse>>()).Should().BeEmpty();
		}

		[Fact]
		public async Task GetTimesheet_WhenExists_ReturnsTheTimesheet()
		{
			// Arange
			var token = await AuthenticateAsync();
			var serializationOptions = new JsonSerializerOptions();
			serializationOptions.Converters.Add(new DateOnlyJsonConverter());
			serializationOptions.Converters.Add(new TimeOnlyJsonConverter());
			var userId = GetUserIdFromJwt(token);

			// Act
			var response = await HttpClient.PostAsJsonAsync(ApiV1Routes.Timesheets.Create, new CreateTimesheetRequest()
			{
				Date = DateOnly.FromDateTime(DateTime.Now),
				Description = "Test timesheet",
				IsAbsent = false,
				IsHoliday = false,
				TimeIn = DateTime.Now.AddHours(8).AddMinutes(30),
				TimeOut = DateTime.Now.AddHours(17).AddMinutes(30),
				UserId = userId,
			}, serializationOptions);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Created);
			var timesheetResponse = await response.Content.ReadFromJsonAsync<CreateTimesheetResponse>();
			timesheetResponse.Should().NotBeNull();
			timesheetResponse.UserId.Should().Be(userId);
			timesheetResponse.Description.Should().Be("Test timesheet");
		}
	}
}