﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace Linkout.Data
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class LinkoutEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new LinkoutEntities object using the connection string found in the 'LinkoutEntities' section of the application configuration file.
        /// </summary>
        public LinkoutEntities() : base("name=LinkoutEntities", "LinkoutEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new LinkoutEntities object.
        /// </summary>
        public LinkoutEntities(string connectionString) : base(connectionString, "LinkoutEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new LinkoutEntities object.
        /// </summary>
        public LinkoutEntities(EntityConnection connection) : base(connection, "LinkoutEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<CostcoProduct> CostcoProducts
        {
            get
            {
                if ((_CostcoProducts == null))
                {
                    _CostcoProducts = base.CreateObjectSet<CostcoProduct>("CostcoProducts");
                }
                return _CostcoProducts;
            }
        }
        private ObjectSet<CostcoProduct> _CostcoProducts;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the CostcoProducts EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCostcoProducts(CostcoProduct costcoProduct)
        {
            base.AddObject("CostcoProducts", costcoProduct);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Linkout.Data", Name="CostcoProduct")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class CostcoProduct : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new CostcoProduct object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="price">Initial value of the Price property.</param>
        /// <param name="activeFlag">Initial value of the ActiveFlag property.</param>
        public static CostcoProduct CreateCostcoProduct(global::System.Decimal id, global::System.String code, global::System.String description, global::System.Decimal price, global::System.String activeFlag)
        {
            CostcoProduct costcoProduct = new CostcoProduct();
            costcoProduct.ID = id;
            costcoProduct.Code = code;
            costcoProduct.Description = description;
            costcoProduct.Price = price;
            costcoProduct.ActiveFlag = activeFlag;
            return costcoProduct;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Decimal _ID;
        partial void OnIDChanging(global::System.Decimal value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> LeatherRows
        {
            get
            {
                return _LeatherRows;
            }
            set
            {
                OnLeatherRowsChanging(value);
                ReportPropertyChanging("LeatherRows");
                _LeatherRows = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LeatherRows");
                OnLeatherRowsChanged();
            }
        }
        private Nullable<global::System.Decimal> _LeatherRows;
        partial void OnLeatherRowsChanging(Nullable<global::System.Decimal> value);
        partial void OnLeatherRowsChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> SeatHeaters
        {
            get
            {
                return _SeatHeaters;
            }
            set
            {
                OnSeatHeatersChanging(value);
                ReportPropertyChanging("SeatHeaters");
                _SeatHeaters = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SeatHeaters");
                OnSeatHeatersChanged();
            }
        }
        private Nullable<global::System.Decimal> _SeatHeaters;
        partial void OnSeatHeatersChanging(Nullable<global::System.Decimal> value);
        partial void OnSeatHeatersChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code;
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                OnPriceChanging(value);
                ReportPropertyChanging("Price");
                _Price = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Price");
                OnPriceChanged();
            }
        }
        private global::System.Decimal _Price;
        partial void OnPriceChanging(global::System.Decimal value);
        partial void OnPriceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ActiveFlag
        {
            get
            {
                return _ActiveFlag;
            }
            set
            {
                OnActiveFlagChanging(value);
                ReportPropertyChanging("ActiveFlag");
                _ActiveFlag = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ActiveFlag");
                OnActiveFlagChanged();
            }
        }
        private global::System.String _ActiveFlag;
        partial void OnActiveFlagChanging(global::System.String value);
        partial void OnActiveFlagChanged();

        #endregion
    
    }

    #endregion
    
}
