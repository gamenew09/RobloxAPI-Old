using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RobloxAPI
{
 
    public class ProductInfo
    {

        /// <summary>
        /// The Id of the Asset.
        /// </summary>
        public int AssetId { get; set; }
        /// <summary>
        /// The Id of the Product 0 if it is not a product or a place.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The thumbnail of the product
        /// </summary>
        public Image Thumbnail
        {
            get
            {
                return RobloxApi.GetThumbnailImage(this, 110, 110);
            }
        }
        /// <summary>
        /// Name of the Product/Asset
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the Product/Asset
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Type of Asset
        /// </summary>
        /// <seealso cref="RobloxAssetType"/>
        public int AssetTypeId { get; set; }
        /// <summary>
        /// The person who created the Asset/Product.
        /// </summary>
        public User Creator { get; set; }
        /// <summary>
        /// Date when the Asset/Product was created.
        /// </summary>
        public string Created { get; set; }
        /// <summary>
        /// Date when the Asset/Product was last updated.
        /// </summary>
        public string Updated { get; set; }
        /// <summary>
        /// The Price In Robux, sometimes returns null.
        /// </summary>
        public object PriceInRobux { get; set; }
        /// <summary>
        /// The Price In Tickets, sometimes returns null.
        /// </summary>
        public object PriceInTickets { get; set; }
        /// <summary>
        /// The sales number of the item
        /// </summary>
        public int Sales { get; set; }
        /// <summary>
        /// Is the item marked as new.
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// Is the item marked on sale?
        /// </summary>
        public bool IsForSale { get; set; }
        /// <summary>
        /// Is the item free?
        /// </summary>
        public bool IsPublicDomain { get; set; }
        /// <summary>
        /// Does the item have limited stock.
        /// </summary>
        public bool IsLimited { get; set; }
        /// <summary>
        /// Is the item limited u?
        /// </summary>
        public bool IsLimitedUnique { get; set; }
        /// <summary>
        /// How many items are remaining to sell? Can be null.
        /// </summary>
        public object Remaining { get; set; }
        /// <summary>
        /// Do you need Builders Club?
        /// </summary>
        public int MinimumMembershipLevel { get; set; }
        /// <summary>
        /// The required age.
        /// 0 = Everyone
        /// 1 = 13+
        /// </summary>
        public int ContentRatingTypeId { get; set; }
        /// <summary>
        /// Tells if this is a Developer Product.
        /// </summary>
        public bool IsDeveloperProduct { get  { return ProductId != 0 && AssetId == 0; } }

    }
}
