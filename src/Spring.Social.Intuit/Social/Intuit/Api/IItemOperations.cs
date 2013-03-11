using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api
{
    public interface IItemOperations
    {
        Task<Item> GetItem(long id);
        Item[] GetItems();
        Task<Item> Update(Item item);
        Task<Item> Create(Item item);
        /// <summary>
        /// Depending on if there is a idType create or update is called. Essentially
        /// a wrapper around create and update to hide the logic.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="Item"/>object representing an Intuit Item.
        /// </returns>
        /// <exception cref="IntuitApiException">If there is an error while communicating with Intuit.</exception>
        Task<Item> Save(Item item);
        bool Delete(Item item);
    }
}
