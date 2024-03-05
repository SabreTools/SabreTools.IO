namespace SabreTools.IO.Readers
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

    /// <summary>
    /// Different types of INI rows being parsed
    /// </summary>
    public enum IniRowType
    {
        None,
        SectionHeader,
        KeyValue,
        Comment,
        Invalid,
    }
}
