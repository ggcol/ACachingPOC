using Shared.Entities;

namespace Shared.Responses.Programmers;

// public sealed record GetResponse(IEnumerable<Programmer> Programmers);

public sealed record GetResponse<T>(IEnumerable<T>? Programmers) where T : Programmer;