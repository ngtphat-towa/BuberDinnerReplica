
using FluentResults;

namespace BuberDinner.Application.Common.Errors;

public record DuplicatedEmailError : IError
{
    public List<IError> Reasons => [];

    public string Message => "Duplicated email";

    public Dictionary<string, object> Metadata => [];
}