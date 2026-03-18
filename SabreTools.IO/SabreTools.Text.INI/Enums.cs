namespace SabreTools.Text.INI
{
    /// <summary>
    /// Different types of INI rows being parsed
    /// </summary>
    public enum RowType
    {
        None,
        SectionHeader,
        KeyValue,
        Comment,
        Invalid,
    }
}
