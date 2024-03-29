﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    public class JsonPokemonLoader
    {
        public List<BasePokemon> LoadAllPokemon()
        {
            List<BasePokemon> thePokemon = new List<BasePokemon>();
            string appPath = Application.dataPath;
            string pokemonDataPath = appPath + "/Resources/Pokemon";
            string fileExtension = ".json";

            string[] files = Directory.GetFiles(pokemonDataPath, "*" + fileExtension);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string id = fileName.Substring(0, 3);

                string jsonString = File.ReadAllText(file);
                JsonPokemonData data = JsonUtility.FromJson<JsonPokemonData>(jsonString);

                BasePokemon pokemon = CreatePokemonFromData(data, id);

                thePokemon.Add(pokemon);
            }

            return thePokemon;
        }

        public Sprite GetPokemonSprite(string id)
        {
            string appPath = Application.dataPath;
            string spriteFilePath = appPath + "/Resources/Images/" + id + ".png";

            byte[] fileData = File.ReadAllBytes(spriteFilePath);

            Texture2D pokemonTexture = new Texture2D(2, 2);
            pokemonTexture.LoadImage(fileData);

            if (pokemonTexture == null)
            {
                Debug.LogError("Could not load sprite texture at path: " + spriteFilePath);
                return null;
            }

            Sprite sprite = Sprite.Create(pokemonTexture, new Rect(0, 0, pokemonTexture.width, pokemonTexture.height), Vector2.zero);

            return sprite;
        }

        private BasePokemon CreatePokemonFromData(JsonPokemonData data, string id)
        {
            BasePokemon pokemon = new BasePokemon();

            pokemon.Id = id;
            pokemon.Name = data.name;
            pokemon.Description = data.description;
            pokemon.BaseHealth = data.health;
            pokemon.BaseAttack = data.attack;
            pokemon.BaseDefense = data.defense;
            pokemon.BaseSpecialAttack = data.specialAttack;
            pokemon.BaseSpecialDefense = data.specialDefense;
            pokemon.BaseSpeed = data.speed;

            return pokemon;
        }
    }
}
