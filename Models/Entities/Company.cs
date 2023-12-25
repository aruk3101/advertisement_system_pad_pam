using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.UI.Xaml.CustomAttributes;
using Projekt.Models.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("companies")]
    public class Company : ValidatableEntity, IEquatable<Company>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PictureSource { get; set; }

        private string bannerColor;

        public string BannerColor
        {
            get { return bannerColor; }
            set {
                bannerColor = value;
                if (bannerColorBrush == null) bannerColorBrush = new ColorObject();
                bannerColorBrush.Color = Color.FromUint(Convert.ToUInt32(BannerColor));
            }
        }


        private ColorObject bannerColorBrush;
        [Ignore]
        public ColorObject BannerColorBrush
        {
            get { return bannerColorBrush; }
            set { 
                bannerColorBrush = value;
                if(value != null)
                {
                    BannerColor = bannerColorBrush.Color.ToUint().ToString();
                }
            }
        }


        public bool Equals(Company? other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
    }
}
