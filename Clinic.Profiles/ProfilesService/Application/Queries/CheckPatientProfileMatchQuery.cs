using MediatR;
using ProfilesService.Application.DTO;

namespace ProfilesService.Application.Queries;

public record CheckPatientProfileMatchQuery(
    string FirstName,
    string LastName,
    string? MiddleName,
    DateTime DateOfBirth):IRequest<ProfileMatchResult>;
    
public record ProfileMatchResult
{
    public bool IsMatchFound { get; set; }
    public MatchedProfileDto? MatchedProfile { get; set; } 
}