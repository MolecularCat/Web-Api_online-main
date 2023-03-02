﻿using Dapper;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Web_Api.online.Models.Tables;
using Web_Api.online.Models.ViewModels;

namespace Web_Api.online.Data.Repositories
{
    public class UsersInfoRepository
    {
        private readonly IDbConnection _db;
        public UsersInfoRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        public async Task<List<RegistratedUsersTableModel>> GetAllRegistratedUsers()
        {
            try
            {
                List<RegistratedUsersTableModel> result =
                    (List<RegistratedUsersTableModel>)await _db.QueryAsync<RegistratedUsersTableModel>
                    ("GetAllRegistratedUser",
                        commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> GetCountOfRegistratedUsers()
        {
            try
            {
                return await _db.QueryFirstAsync<int>(
                    "GetCountOfRegistratedUsers",
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception exc)
            {
                return 0;
            }
        }

        public async Task<List<RegistratedUsersTableModel>> GetRegistratedUsersPaged(int page, int pageSize, int currentPage=1)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("page", page);
                p.Add("pageSize", pageSize);

                List<RegistratedUsersTableModel> result =
                    (List<RegistratedUsersTableModel>)await _db.QueryAsync<RegistratedUsersTableModel>
                    ("GetRegistratedUsers_Paged",
                        p,
                        commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task CreateEmptyUsersInfo(string userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);

                await _db.ExecuteAsync(
                    "CreateEmptyUsersInfo",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            } 
            catch (Exception exc) { }
        }
        
        public async Task<UserInfoTableModel> GetUserInfo(string userId)
        {
            try
            {
                return await _db.QueryFirstAsync<UserInfoTableModel>(
                    "GetUserInfo_ByUserId",
                    new
                    {
                        userId = userId
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task spCreateOrUpdateUsersInfoLocation(UserInfoTableModel model)
        {
            try
            {
                await _db.ExecuteAsync(
                    "CreateOrUpdateUsersInfoLocation",
                    new
                    {
                        userId = model.UserId,
                        location = model.Location
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { }
        }

        public async Task spCreateOrUpdateProfileUsersInfoPhoto(UserInfoTableModel model)
        {
            try
            {
                await _db.ExecuteAsync(
                    "CreateOrUpdateProfileUsersInfoPhoto",
                    new
                    {
                        userId = model.UserId,
                        profilePhotoPath = model.ProfilePhotoPath
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { }
        }

        public async Task spCreateOrUpdateProfileUserInfo(UserInfoTableModel model)
        {
            try
            {
                await _db.ExecuteAsync(
                    "CreateOrUpdateProfileUserInfo",
                    new
                    {
                        userId = model.UserId,
                        fullName = model.FullName,
                        aboutMe = model.AboutMe,
                        facebookLink = model.FacebookLink,
                        instagramLink = model.InstagramLink,
                        skypeLink = model.SkypeLink,
                        twitterLink = model.TwitterLink,
                        linkedinLink = model.LinkedinLink,
                        githubLink = model.GithubLink
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { }
        }

        public async Task SetUsersInfoRefid(string userId, string refid)
        {
            try
            {
                await _db.ExecuteAsync(
                    "SetUserInfoRefid",
                    new
                    {
                        userId = userId,
                        refid = refid
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { }
        }
    }
}
