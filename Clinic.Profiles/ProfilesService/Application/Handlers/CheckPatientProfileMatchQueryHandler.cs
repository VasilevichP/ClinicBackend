using MediatR;
using ProfilesService.Application.DTO;
using ProfilesService.Application.Queries;
using ProfilesService.Domain.Interfaces;

namespace ProfilesService.Application.Handlers;

public class CheckPatientProfileMatchQueryHandler: IRequestHandler<CheckPatientProfileMatchQuery, ProfileMatchResult>
{
    private readonly IPatientRepository _repository;

    public CheckPatientProfileMatchQueryHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProfileMatchResult> Handle(CheckPatientProfileMatchQuery request, CancellationToken cancellationToken)
    {
        var unlinkedProfiles = await _repository.GetUnlinkedProfilesAsync(cancellationToken);

        foreach (var profile in unlinkedProfiles)
        {
            int matchScore = 0;

            if (string.Equals(profile.FirstName, request.FirstName, StringComparison.OrdinalIgnoreCase)) matchScore += 5;
            if (string.Equals(profile.LastName, request.LastName, StringComparison.OrdinalIgnoreCase)) matchScore += 5;
            
            if (!string.IsNullOrEmpty(request.MiddleName) && 
                string.Equals(profile.MiddleName, request.MiddleName, StringComparison.OrdinalIgnoreCase)) matchScore += 5;

            if (profile.DateOfBirth == request.DateOfBirth.Date) matchScore += 3;

            if (matchScore >= 13)
            {
                return new ProfileMatchResult 
                { 
                    IsMatchFound = true, 
                    MatchedProfile = new MatchedProfileDto
                    {
                        Id = profile.Id,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        MiddleName = profile.MiddleName,
                        DateOfBirth = profile.DateOfBirth
                    }
                };
            }
        }
        
        return new ProfileMatchResult { IsMatchFound = false };
    }
}