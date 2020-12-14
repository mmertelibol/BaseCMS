using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class ActionLogDto
    {
        public int Id { get; set; }

        public DateTime RequestStart { get; set; }

        public DateTime RequestEnd { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Parameters { get; set; }

        public int? UserId { get; set; }

        public string Referer { get; set; }

        public string ClientIp { get; set; }

        public decimal ElapsedTimeInSeconds { get; set; }
    }

    public class PartialDto
    {
        public PartialDto()
        {
            FormItems = new List<PartialItemDto>();
        }

        public List<PartialItemDto> FormItems { get; set; }

        public List<int> Divs { get; set; }
    }

    public class PartialItemDto
    {
        public string PropVisibleName { get; set; }

        public string PropName { get; set; }

        public dynamic PropVal { get; set; }

        public string HtmlId { get; set; }

        public string PropType { get; set; }

        public string HtmlType { get; set; }

        public string Attributes { get; set; }

        public string Classes { get; set; }

        public int Side { get; set; }

        public bool Readonly { get; set; }

        public bool IsStringSelect { get; set; }

        public List<LookupDto> List { get; set; }
    }

    public class TokenRequestDto
    {
        public int SapCode { get; set; }

        public string ApiKey { get; set; }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }

        public string Redirect { get; set; }
    }

    public class PageDto
    {
        public int Id { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string TitleCode { get; set; }

        public int DisplayOrder { get; set; }
        public string MenuIcon { get; set; }

        public int PageLevel { get; set; }

        public int? ParentId { get; set; }

        public bool HideInMenu { get; set; }
        public string Href { get; set; }
    }

    public class LookupDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Code { get; set; }

        public string Category { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }

    public interface IKeyedObject
    {
        int Id { get; set; }
    }

    public enum ApiLoginResultCode
    {
        Success = 101,
        WarningNoWebApi = 201,
        WarningInvalidContractDate = 202,
        FailInvalidApiKey = 301,
    }

    public class ApiLoginResultDto
    {
        public ApiLoginResultCode ResultCode { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public int? SapCode { get; set; }

        public int? ApiUserId { get; set; }
    }
    public class ConstantDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DescriptionCode { get; set; }
        public string GroupCode { get; set; }
        public string ParentGroupCode { get; set; }

        public DateTime? AddedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int DisplayOrder { get; set; }
    }
}