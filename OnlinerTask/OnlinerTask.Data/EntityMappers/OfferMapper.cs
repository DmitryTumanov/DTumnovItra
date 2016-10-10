﻿using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlinerTask.Data.EntityMappers
{
    public class OfferMapper
    {
        public OfferMapper() { }

        public OffersModel ConvertToModel(Offer dbmodel)
        {
            return new OffersModel()
            {
                Count = (int)dbmodel.Count
            };
        }

        public Offer ConvertToModel(OffersModel model)
        {
            return new Offer()
            {
                Count = (int)model.Count
            };
        }
    }
}