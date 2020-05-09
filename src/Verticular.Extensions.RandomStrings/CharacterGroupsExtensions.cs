using Verticular.Extensions.RandomStrings;

namespace System
{
  /// <summary>
  /// Wraps the conversion from character groups to a character array.
  /// </summary>
  public static class CharacterGroupsExtensions
  {
    /// <summary>
    /// Converts the selected <see cref="CharacterGroups"/> to a char array.
    /// </summary>
    /// <param name="groups">The selected character groups.</param>
    /// <returns>A char array representing the selected character groups.</returns>
    public static char[] ToCharArray(this CharacterGroups groups)
    {
      // we use an extension method so that we do not have to make the whole class public.
      return CharacterGroup.Get(groups);
    }
  }
}
