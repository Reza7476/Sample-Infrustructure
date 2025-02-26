namespace Sample.Persistence.EF;

public class InfrastructureHelper
{
    public static string? GetInfrastructureDirectory()
    {

        string currentDirectory=Directory.GetCurrentDirectory();
        
        DirectoryInfo? parentDir=Directory.GetParent(currentDirectory);
        
        var slnFile = parentDir.GetFiles("*.sln");
        
        parentDir=parentDir.Parent;
        
        //string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        
        string infrastructureProjectName = "Infrastructure\\Sample.Persistence.EF";
        
        var infrastructureDirectory = parentDir + "\\" + infrastructureProjectName;

        return infrastructureDirectory;
    }
}
