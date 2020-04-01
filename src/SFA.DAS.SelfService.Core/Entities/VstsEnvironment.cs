namespace SFA.DAS.SelfService.Core.Entities
{
    public class VstsEnvironment
    {
        //The Id for the environment for the Release Definition
        public int DefinitionId { get; set; }

        //The Id for the environment specific to a created Release
        public int EnvironmentReleaseId { get; set; }

        public string Name { get; set; }
    }
}