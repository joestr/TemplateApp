using Newtonsoft.Json;
using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialTree : WebAppComponentViewModel
    {
        public List<PartialTreeEntry> TreeEntries { get; set; }
        public string[]? OpenedTreeIds { get; set; }

        public PartialTree(string identifier, IDictionary<string, string> hiddenInputNameAndValues, IQueryCollection queryCollection, List<PartialTreeEntry> treeEntries, string[]? openedTreeIds) : base(identifier, hiddenInputNameAndValues, queryCollection)
        {
            TreeEntries = treeEntries;
            OpenedTreeIds = openedTreeIds;
            FillFromQuery();
        }

        public override void FillFromQuery(IQueryCollection queryCollection)
        {
            OpenedTreeIds = GetStringArrayFromQueryParameter(queryCollection, nameof(OpenedTreeIds));
        }

        public override List<KeyValuePair<string, string>> GetHiddenInputNameAndValues()
        {
            var result = new List<KeyValuePair<string, string>>();
            result = this.AddNullableToList(result, this.GetHiddenInputNameAndValue(nameof(OpenedTreeIds), OpenedTreeIds));
            return result;
        }

        public List<PartialTreeEntry> GetRootTreeEntries()
        {
            return TreeEntries.FindAll(entry => entry.ParentId == null);
        }

        public List<PartialTreeEntry> GetChildTreeEntries(string parentId)
        {
            return TreeEntries.FindAll(entry => entry.ParentId == parentId);
        }

        public string GetOpenedTreeIdsWithTreeId(string treeId)
        {
            var result = new List<string>();

            if (OpenedTreeIds != null)
            {
                result = new List<string>(OpenedTreeIds);
                result.Add(treeId);
            }

            return JsonConvert.SerializeObject(result.ToArray());
        }

        public string GetOpenedTreeIdsWithoutTreeId(string treeId)
        {
            var result = new List<string>();

            if (OpenedTreeIds != null)
            {
                result = new List<string>(OpenedTreeIds);
                result.Remove(treeId);
            }

            return JsonConvert.SerializeObject(result.ToArray());
        }
    }
}
