﻿using Azure;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebOopPrac_Api.Models;
using WebOopPrac_Api.Repository;
using static WebOopPrac_Api.Models.ServiceResponse;
using static WebOopPrac_Api.Models.TodoModel;

namespace WebOopPrac_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TaskController : ControllerBase
    {
        private readonly IAccount _acc;

        public TaskController(IAccount acc)
        {
            _acc = acc;
        }

        [HttpPost]
        [Route("InsertData/{Title}/{Description}/{IsCompleted}")]
        public async Task<IActionResult> CredInsert(string Title, string Description, bool IsCompleted)
        {
            try
            {
                ServiceResponseModel<IEnumerable<dynamic>> Response = await _acc.CredInsert(Title, Description, IsCompleted);

                return StatusCode((int)Response.HttpCode, Response);
            }
            catch (Exception ee)
            {
                return StatusCode(500, new { message = "Internal Server Error" });

            }
        }

        [HttpGet]
        [Route("GetAllData")]
        public async Task<IActionResult> AllTodos()

        {
            var Response = new ServiceResponseModel<IEnumerable<dynamic>>();

            try
            {
                if (ModelState.IsValid)
                {
                    var todoData = await _acc.GetTodoModel();

                    Response.Data = todoData;
                    Response.HttpCode = ServiceResponseStatusCode.Success;
                    Response.ResponseCode = ServiceResponseCode.Success;
                }
                else
                {
                    Response.HttpCode = ServiceResponseStatusCode.InternalError;
                    Response.ResponseCode = ServiceResponseCode.SqlError;
                    Response.DeveloperMessage = "Invalid Cred empty";
                    Response.UserMessage = "Please supply all required fields.";
                }
            }
            catch (Exception ee)
            {
                Response.HttpCode = ServiceResponseStatusCode.InternalError;
                Response.ResponseCode = ServiceResponseCode.SqlError;
                Response.DeveloperMessage = ee.Message;
                Response.UserMessage = "It seems something went wrong. Please try again.";
            }
            return StatusCode((int)Response.HttpCode, Response);
        }

        [HttpGet]
        [Route("GetSingleData/{UniqueID}")]
        public async Task<IActionResult> GetOneData(int UniqueID)
        {
            var Response = new ServiceResponseModel<IEnumerable<dynamic>>();

            try
            {
                var getOneData = await _acc.GetOneData(UniqueID);

                if (getOneData != null)
                {
                    Response.Data = getOneData;
                    Response.HttpCode = ServiceResponseStatusCode.Success;
                    Response.ResponseCode = ServiceResponseCode.Success;
                }
                else
                {
                    Response.HttpCode = ServiceResponseStatusCode.InternalError;
                    Response.ResponseCode = ServiceResponseCode.SqlError;
                    Response.DeveloperMessage = "Invalid Cred empty";
                    Response.UserMessage = "Please supply all required fields.";
                }
            }
            catch (Exception ee)
            {
                Response.HttpCode = ServiceResponseStatusCode.InternalError;
                Response.ResponseCode = ServiceResponseCode.SqlError;
                Response.DeveloperMessage = ee.Message;
                Response.UserMessage = "It seems something went wrong. Please try again.";
            }
            return StatusCode((int)Response.HttpCode, Response);
        }

        [HttpPut]
        [Route("UpdateData/{Id}/{Title}/{Description}/{IsCompleted}")]
        public async Task<IActionResult> UpdateData(int Id, string Title, string Description, bool IsCompleted)
        {
            try
            {
                ServiceResponseModel<IEnumerable<dynamic>> Response = await _acc.UpdateData(Id, Title, Description, IsCompleted);

                return StatusCode((int)Response.HttpCode, Response);
            }
            catch (Exception ee)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpDelete]
        [Route("DeleteData/{Id}")]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            try
            {
                ServiceResponseModel<IEnumerable<dynamic>> Response = await _acc.DeleteItem(Id);

                return StatusCode((int)Response.HttpCode, Response);
            }
            catch (Exception ee)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }


    }
}

