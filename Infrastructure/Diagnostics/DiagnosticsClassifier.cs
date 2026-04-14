namespace Infrastructure.Diagnostics;

public static class DiagnosticsClassifier
{
    public static bool IsService(Type t)
        => t.Namespace?.Contains(".Services") == true;

    public static bool IsUseCase(Type t)
        => t.Name.EndsWith("UseCase");

    public static bool IsHandler(Type t)
        => t.Name.EndsWith("Handler");

    public static bool IsValidator(Type t)
        => t.Name.EndsWith("Validator");

    public static bool IsRepository(Type t)
        => t.Name.EndsWith("Repository");
}