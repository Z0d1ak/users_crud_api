using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Contracts;
using app.Contracts.Dtos;
using app.Data.Entities;
using app.Shared.Exceptions;

namespace app.Converters
{
    public static class ConverterExtensions
    {
        public static UserDto ToDto(this User model)
        {
            return new UserDto
            {
                Id = model.Id,
                Name = model.Name,
                Status = (StatusDto)(int)model.Status,
            };
        }

        public static User ToModel(this UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Status = (Status)(int)dto.Status,
            };
        }

        public static Status ToModel(this StatusDto dto) => (Status)(int)dto;

        public static ErrorCodeDto ToDto(this ErrorCode model) => (ErrorCodeDto)(int)model;

    }
}
