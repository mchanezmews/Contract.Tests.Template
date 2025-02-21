namespace Consumer.Tests;

public class ContractsPublish
{
    public string PacticipantName { get; set; }
    public string PacticipantVersionNumber { get; set; }
    public List<string> Tags { get; set; }
    public string Branch { get; set; }
    public string BuildUrl { get; set; }
    public List<Contract> Contracts { get; set; }
}