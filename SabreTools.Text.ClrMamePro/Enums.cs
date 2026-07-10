namespace SabreTools.Text.ClrMamePro
{
    /// <summary>
    /// Different types of CMP rows being parsed
    /// </summary>
    public enum CmpRowType
    {
        None = 0,
        TopLevel = 1,
        Standalone = 2,
        Internal = 3,
        Comment = 4,
        EndTopLevel = 5,
    }
}
