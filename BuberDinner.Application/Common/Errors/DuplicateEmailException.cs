namespace BuberDinner.Application.Common.Errors;

public class DuplicateEmailException(string? message = "The give email already existed") : Exception(message)
{
}