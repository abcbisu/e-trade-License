using System;
using System.ComponentModel.DataAnnotations;

namespace etrade.entities
{
    public class Document : IDataManage
    {
        public long Id { get; set; }
        /// <summary>
        /// Path is rest directory after base-dir. ex=>\uploads\profile_pic
        /// </summary>
        [StringLength(500)]
        public string Path { get; set; }
        /// <summary>
        /// only document name with extension
        /// </summary>
        [StringLength(100)]
        public string DocName { get; set; }
        /// <summary>
        /// base address like server address. ex=> http:80\\server.com\
        /// </summary>
        [StringLength(900)]
        public string BaseDir { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        public string GetWellKnownFileName()
        {
            return System.IO.Path.Combine(this.BaseDir, this.Path, this.DocName);
        }
    }

}
