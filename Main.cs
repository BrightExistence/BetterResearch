using BrightExistence.SimpleTools;
using Pipliz;
using Pipliz.JSON;
using Server.Localization;
using System;
using System.Collections.Generic;
using System.IO;

namespace BrightExistence.BetterResearch
{
    [ModLoader.ModManagerAttribute]
    public static class Main
    {
        public const string NAMESPACE = "BrightExistence.BetterResearch";

        private static SimpleItem BasicResearchEquipment = new SimpleItem("basicresearchequipment", "BrightExistence.LogicalResearch", true);

        private static SimpleItem InfrastructureComponents = new SimpleItem("infrastructurecomponents", "BrightExistence.LogicalResearch", true);

        private static SimpleItem AlchemyIngredients = new SimpleItem("alchemyingredients", "BrightExistence.LogicalResearch", true);

        private static SimpleItem AdvancedComponenents = new SimpleItem("advancedcomponents", "BrightExistence.LogicalResearch", true);

        private static SimpleItem MilitaryParts = new SimpleItem("militaryparts", "BrightExistence.LogicalResearch", true);

        private static SimpleItem IronParts = new SimpleItem("ironparts", "", true);

        private static SimpleItem IronTools = new SimpleItem("irontools", "", true);

        private static SimpleRecipe RecResearchEquipment = new SimpleRecipe(Main.BasicResearchEquipment, "pipliz.technologist");

        private static SimpleRecipe RecInfrastructureComponents = new SimpleRecipe(Main.InfrastructureComponents, "pipliz.technologist");

        private static SimpleRecipe RecAlchemyIngredients = new SimpleRecipe(Main.AlchemyIngredients, "pipliz.technologist");

        private static SimpleRecipe RecAdvancedComponents = new SimpleRecipe(Main.AdvancedComponenents, "pipliz.technologist");

        private static SimpleRecipe RecMilitaryParts = new SimpleRecipe(Main.MilitaryParts, "pipliz.technologist");

        private static SimpleRecipe RecIronParts = new SimpleRecipe(Main.IronParts, "pipliz.metalsmith");

        private static SimpleRecipe RecIronTools = new SimpleRecipe(Main.IronTools, "pipliz.metalsmith");

        private static SimpleResearchable ResBasicResearchEquipment = new SimpleResearchable("BasicResearchEquipment", "BrightExistence.LogicalResearch");

        private static SimpleResearchable ResAlchemyIngredients = new SimpleResearchable("AlchemyIngregients", "BrightExistence.LogicalResearch");

        private static SimpleResearchable ResInfrastructureComponents = new SimpleResearchable("InfrastructureComponents", "BrightExistence.LogicalResearch");

        private static SimpleResearchable ResMilitaryParts = new SimpleResearchable("MilitaryParts", "BrightExistence.LogicalResearch");

        private static SimpleResearchable ResAdvancedComponents = new SimpleResearchable("AdvancedComponents", "BrightExistence.LogicalResearch");

        [ModLoader.ModCallbackAttribute(ModLoader.EModCallbackType.OnAssemblyLoaded, "BrightExistence.LogicalResearch.OnAssemblyLoaded")]
        public static void OnAssemblyLoaded(string path)
        {
            Log.Write<string>("Mod {0} loaded.", "BrightExistence.LogicalResearch");
            Log.Write<string, string>("{0}: Utilizing SimpleTools version: {1}", "BrightExistence.LogicalResearch", Variables.toolkitVersion);
            Log.Write<string>("{0}: Credit and thanks to Pandaros for the localization code!", "BrightExistence.LogicalResearch");
            Variables.modDirectory = Path.GetDirectoryName(path).Replace("\\", "/");
        }

        [ModLoader.ModCallbackAttribute(ModLoader.EModCallbackType.AfterSelectedWorld, "BrightExistence.LogicalResearch.afterSelectedWorld"), ModLoader.ModCallbackProvidesForAttribute("pipliz.server.registertexturemappingtextures")]
        public static void afterSelectedWorld()
        {
            UtilityFunctions.registerTextures();
        }

        [ModLoader.ModCallbackAttribute(ModLoader.EModCallbackType.AfterAddingBaseTypes, "BrightExistence.LogicalResearch.afterAddingBaseTypes")]
        public static void afterAddingBaseTypes(Dictionary<string, ItemTypesServer.ItemTypeRaw> items)
        {
            Variables.itemsMaster = items;
            Main.IronParts.Icon = UtilityFunctions.iconPath("ironparts", NAMESPACE);
            Main.IronTools.Icon = UtilityFunctions.iconPath("irontools", NAMESPACE);
            Main.BasicResearchEquipment.maskItem = "sciencebagbasic";
            Main.BasicResearchEquipment.Icon = UtilityFunctions.iconPath("researchEquipment.png", NAMESPACE);
            Main.InfrastructureComponents.maskItem = "sciencebagcolony";
            Main.InfrastructureComponents.Icon = UtilityFunctions.iconPath("infrastructureComponents.png", NAMESPACE);
            Main.AlchemyIngredients.maskItem = "sciencebaglife";
            Main.AlchemyIngredients.Icon = UtilityFunctions.iconPath("alchemyIngredients.png", NAMESPACE);
            Main.MilitaryParts.maskItem = "sciencebagmilitary";
            Main.MilitaryParts.Icon = UtilityFunctions.iconPath("militaryComponents.png", NAMESPACE);
            Main.AdvancedComponenents.maskItem = "sciencebagadvanced";
            Main.AdvancedComponenents.Icon = UtilityFunctions.iconPath("advancedComponents.png", NAMESPACE);

            UtilityFunctions.registerItems(items);
        }

        [ModLoader.ModCallbackAttribute(ModLoader.EModCallbackType.AfterItemTypesDefined, "BrightExistence.LogicalResearch.AfterItemTypesDefined")]
        public static void AfterItemTypesDefined()
        {
            Main.RecIronParts.addRequirement("ironwrought", 5);
            Main.RecIronParts.defaultLimit = 15;
            Main.RecIronTools.addRequirement("ironwrought", 3);
            Main.RecIronTools.addRequirement("planks", 2);
            Main.RecIronTools.defaultLimit = 15;
            Main.RecResearchEquipment.Replaces.Add("pipliz.technologist.sciencebagbasic");
            Main.RecResearchEquipment.addRequirement("coppertools", 1);
            Main.RecResearchEquipment.addRequirement("copperparts", 2);
            Main.RecResearchEquipment.addRequirement("coppernails", 3);
            Main.RecResearchEquipment.defaultLimit = 5;
            //Main.RecResearchEquipment.isOptional = true;
            Main.RecInfrastructureComponents.Replaces.Add("pipliz.technologist.sciencebagcolony");
            Main.RecInfrastructureComponents.addRequirement("coatedplanks", 2);
            Main.RecInfrastructureComponents.addRequirement(Main.IronTools, 1);
            Main.RecInfrastructureComponents.addRequirement("coppernails", 1);
            Main.RecInfrastructureComponents.addRequirement("stonebricks", 2);
            Main.RecInfrastructureComponents.addRequirement("bricks", 2);
            Main.RecInfrastructureComponents.defaultLimit = 5;
            //Main.RecInfrastructureComponents.isOptional = true;
            Main.RecAlchemyIngredients.Replaces.Add("pipliz.technologist.sciencebaglife");
            Main.RecAlchemyIngredients.addRequirement("flour", 1);
            Main.RecAlchemyIngredients.addRequirement("berry", 1);
            Main.RecAlchemyIngredients.addRequirement("wheatstage1", 1);
            Main.RecAlchemyIngredients.addRequirement("flaxstage1", 1);
            Main.RecAlchemyIngredients.defaultLimit = 5;
            //Main.RecAlchemyIngredients.isOptional = true;
            Main.RecMilitaryParts.Replaces.Add("pipliz.technologist.sciencebagmilitary");
            Main.RecMilitaryParts.addRequirement("ironrivet", 2);
            Main.RecMilitaryParts.addRequirement(Main.IronParts, 2);
            Main.RecMilitaryParts.addRequirement("planks", 2);
            Main.RecMilitaryParts.addRequirement(Main.IronTools, 1);
            Main.RecMilitaryParts.defaultLimit = 5;
            //Main.RecMilitaryParts.isOptional = true;
            Main.RecAdvancedComponents.Replaces.Add("pipliz.technologist.sciencebagadvanced");
            Main.RecAdvancedComponents.addRequirement("copperparts", 3);
            Main.RecAdvancedComponents.addRequirement(Main.IronParts, 3);
            Main.RecAdvancedComponents.addRequirement("steelparts", 3);
            Main.RecAdvancedComponents.addRequirement(Main.IronTools, 1);
            Main.RecAdvancedComponents.defaultLimit = 5;

            UtilityFunctions.recipesAndInventoryBlocks();
        }

        [ModLoader.ModCallbackAttribute(ModLoader.EModCallbackType.AfterItemTypesDefined, "BrightExistence.LogicalResearch.AfterDefiningNPCTypes"), ModLoader.ModCallbackProvidesForAttribute("pipliz.apiprovider.jobs.resolvetypes")]
        public static void AfterDefiningNPCTypes()
        {
            //Log.Write<string>("{0}: Job loading complete.", "BrightExistence.LogicalResearch");
        }

        [ModLoader.ModCallbackAttribute(ModLoader.EModCallbackType.OnAddResearchables, "BrightExistence.LogicalResearch.OnAddResearchables"), ModLoader.ModCallbackDependsOnAttribute("pipliz.apiprovider.registerresearchables")]
        public static void OnAddResearchables()
        {
            Main.ResBasicResearchEquipment.Replaces = "pipliz.baseresearch.sciencebagbasic";
            Main.ResBasicResearchEquipment.Icon = UtilityFunctions.iconPath("researchEquipment.png", NAMESPACE);
            Main.ResBasicResearchEquipment.IterationCount = 3;
            Main.ResBasicResearchEquipment.addRequirement("coppertools", 1);
            Main.ResBasicResearchEquipment.addRequirement("bronzeplate", 1);
            Main.ResBasicResearchEquipment.addRequirement("copperparts", 3);
            Main.ResBasicResearchEquipment.Dependencies.Add("pipliz.baseresearch.technologisttable");
            Main.ResBasicResearchEquipment.Unlocks.Add(new SimpleResearchable.Unlock(Main.RecResearchEquipment));
            Main.ResAlchemyIngredients.Replaces = "pipliz.baseresearch.sciencebaglife";
            Main.ResAlchemyIngredients.Icon = UtilityFunctions.iconPath("alchemyIngredients.png", NAMESPACE);
            Main.ResAlchemyIngredients.IterationCount = 3;
            Main.ResAlchemyIngredients.addRequirement("flour", 1);
            Main.ResAlchemyIngredients.addRequirement("berry", 1);
            Main.ResAlchemyIngredients.addRequirement("wheatstage1", 1);
            Main.ResAlchemyIngredients.addRequirement("flaxstage1", 1);
            Main.ResAlchemyIngredients.Dependencies.Add("pipliz.baseresearch.technologisttable");
            Main.ResAlchemyIngredients.Dependencies.Add("pipliz.baseresearch.wheatfarming");
            Main.ResAlchemyIngredients.Unlocks.Add(new SimpleResearchable.Unlock(Main.RecAlchemyIngredients));
            Main.ResInfrastructureComponents.Replaces = "pipliz.baseresearch.sciencebagcolony";
            Main.ResInfrastructureComponents.Icon = UtilityFunctions.iconPath("infrastructureComponents.png", NAMESPACE);
            Main.ResInfrastructureComponents.IterationCount = 5;
            Main.ResInfrastructureComponents.addRequirement("coatedplanks", 2);
            Main.ResInfrastructureComponents.addRequirement(Main.IronTools, 1);
            Main.ResInfrastructureComponents.addRequirement("coppernails", 1);
            Main.ResInfrastructureComponents.addRequirement("stonebricks", 2);
            Main.ResInfrastructureComponents.addRequirement("bricks", 2);
            Main.ResInfrastructureComponents.Dependencies.Add("pipliz.baseresearch.matchlockgun");
            Main.ResInfrastructureComponents.Unlocks.Add(new SimpleResearchable.Unlock(Main.RecInfrastructureComponents));
            Main.ResMilitaryParts.Replaces = "pipliz.baseresearch.sciencebagmilitary";
            Main.ResMilitaryParts.Icon = UtilityFunctions.iconPath("militaryComponents.png", NAMESPACE);
            Main.ResMilitaryParts.IterationCount = 3;
            Main.ResMilitaryParts.addRequirement("ironrivet", 2);
            Main.ResMilitaryParts.addRequirement(Main.IronParts, 2);
            Main.ResMilitaryParts.addRequirement("planks", 2);
            Main.ResMilitaryParts.addRequirement(Main.IronTools, 1);
            Main.ResMilitaryParts.Dependencies.Add("pipliz.baseresearch.technologisttable");
            Main.ResMilitaryParts.Dependencies.Add("pipliz.baseresearch.bloomery");
            Main.ResMilitaryParts.Unlocks.Add(new SimpleResearchable.Unlock(Main.RecMilitaryParts));
            Main.ResAdvancedComponents.Replaces = "pipliz.baseresearch.sciencebagadvanced";
            Main.ResAdvancedComponents.Icon = UtilityFunctions.iconPath("advancedComponents.png", NAMESPACE);
            Main.ResAdvancedComponents.IterationCount = 5;
            Main.ResAdvancedComponents.addRequirement("copperparts", 3);
            Main.ResAdvancedComponents.addRequirement(Main.IronParts, 3);
            Main.ResAdvancedComponents.addRequirement("steelparts", 3);
            Main.ResAdvancedComponents.addRequirement(Main.IronTools, 1);
            Main.ResAdvancedComponents.Dependencies.Add("pipliz.baseresearch.gunpowder");
            Main.ResAdvancedComponents.Unlocks.Add(new SimpleResearchable.Unlock(Main.RecAdvancedComponents));

            UtilityFunctions.registerResearchables();
        }

        /// <summary>
        /// AfterWorldLoad callback entry point. Used for localization routines.
        /// </summary>
        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad, NAMESPACE == null ? "" : NAMESPACE + ".AfterWorldLoad")]
        [ModLoader.ModCallbackDependsOn("pipliz.server.localization.waitforloading")]
        [ModLoader.ModCallbackProvidesFor("pipliz.server.localization.convert")]
        public static void AfterWorldLoad()
        {
            UtilityFunctions.loadLocalizationFiles();
        }
    }
}
