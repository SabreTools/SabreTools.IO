namespace SabreTools.Text.ClrMamePro
{
    /// <summary>
    /// Different types of CMP rows being parsed
    /// </summary>
    public enum CmpRowType
    {
        None,
        TopLevel,
        Standalone,
        Internal,
        Comment,
        EndTopLevel,
    }
}
