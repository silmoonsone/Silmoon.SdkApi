using Silmoon.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.SdkApi.Aliyun.Models
{
    public class RecognizeIdCardResult
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Sex? Sex { get; set; }
        public string Ethnicity { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdNumber { get; set; }

        public bool IsFaceSide { get; set; }
        /// <summary>
        /// 是否翻拍
        /// </summary>
        public bool? IsReshoot { get; set; }
        /// <summary>
        /// 是否复印件
        /// </summary>
        public bool? IsCopy { get; set; }
        /// <summary>
        /// 完整度评分
        /// </summary>
        public double? CompletenessScore { get; set; }
        /// <summary>
        /// 整体质量评分
        /// </summary>
        public double? QualityScore { get; set; }
        /// <summary>
        /// 篡改指数
        /// </summary>
        public double? TamperScore { get; set; }


        public string IssueAuthority { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
