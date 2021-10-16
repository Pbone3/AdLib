using System.Collections.Generic;

namespace AdLib.Loading
{
    public class EnabledPackList
    {
        public enum ChangeType
        {
            Add,
            Remove
        }

        public HashSet<string> EnabledPacks;
        public HashSet<string> Changes_Additions;
        public HashSet<string> Changes_Removals;

        public bool PendingChanges;

        public EnabledPackList(HashSet<string> initialList)
        {
            EnabledPacks = initialList;
            Changes_Additions = Changes_Removals = new HashSet<string>();

            PendingChanges = false;
        }

        public void SmartSubmitChanges(string change)
        {
            if (EnabledPacks.Contains(change))
                SubmitChange(change, ChangeType.Remove);
            else
                SubmitChange(change, ChangeType.Add);
        }

        public void SubmitChange(string change, ChangeType type)
        {
            if (type == ChangeType.Add)
            {
                if (Changes_Additions.Contains(change))
                    Changes_Additions.Remove(change);
                else
                    Changes_Additions.Add(change);
            }
            else
            {
                if (Changes_Removals.Contains(change))
                    Changes_Removals.Remove(change);
                else
                    Changes_Removals.Add(change);
            }
        }

        public void AcceptNewList()
        {
            EnabledPacks.UnionWith(Changes_Additions);
            EnabledPacks.RemoveWhere(s => Changes_Removals.Contains(s));

            Changes_Additions.Clear();
            Changes_Removals.Clear();

            PendingChanges = false;
        }
    }
}
