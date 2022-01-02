using System;
using System.Collections.Generic;
using DiskCardGame;

namespace APIPlugin
{
	public static class NewTalkingCard
	{
		private const string TalkingCardPortraitPrefab = "Prefabs/Cards/AnimatedPortraits/TalkingCardPortrait";

		private static readonly CharacterFace
			CharacterFaceBase = ResourceBank.Get<CharacterFace>(TalkingCardPortraitPrefab);

		public static readonly Dictionary<string, Type> TalkingCards = new();
		public static readonly List<Type> Types = new(); // For easy access by CardTriggerHandler.GetType

		public static void Add<T>(string name, Dictionary<string, DialogueEvent> dialogueEvents)
		{
			NewTalkingCard.TalkingCards.Add(name.Replace(" ", "_"), typeof(T));
			NewTalkingCard.Types.Add(typeof(T));
			NewDialogue.AddAll(dialogueEvents);
			Plugin.Log.LogInfo($"Added talking card {name}!");
		}

		public static void Add<T>(string name)
		{
			Add<T>(name, new Dictionary<string, DialogueEvent>());
		}


		#region Static_CreateTalkingCardAnimation

		public static UnityEngine.GameObject CreateTalkingCardAnimation(
			List<CharacterFace.EmotionSprites> emotionSpritesList,
			float blinkRate = 3f
		)
		{
			CharacterFace characterFace = CharacterFaceBase;
			characterFace.eyes.blinkRate = blinkRate;
			characterFace.emotionSprites = emotionSpritesList;
			return characterFace.gameObject;
		}

		public static UnityEngine.GameObject CreateTalkingCardAnimation(
			CharacterFace.EmotionSprites emotionSprites,
			float blinkRate = 3f
		)
		{
			return CreateTalkingCardAnimation(
				new List<CharacterFace.EmotionSprites>() { emotionSprites },
				blinkRate
			);
		}

		#endregion

		public static CharacterFace.EmotionSprites CreateSpritesForEmotion(
			Emotion emotion,
			string facePng,
			string eyesOpenPng, string eyesClosedPng,
			string eyesOpenEmissionPng, string eyesClosedEmissionPng,
			string mouthOpenPng, string mouthClosedPng
		)
		{
			return new CharacterFace.EmotionSprites()
			{
				emotion = emotion,
				face = CardUtils.CreateSpriteFromPng(facePng),
				eyesOpen = CardUtils.CreateSpriteFromPng(eyesOpenPng),
				eyesClosed = CardUtils.CreateSpriteFromPng(eyesClosedPng),
				eyesOpenEmission = CardUtils.CreateSpriteFromPng(eyesOpenEmissionPng),
				eyesClosedEmission = CardUtils.CreateSpriteFromPng(eyesClosedEmissionPng),
				mouthOpen = CardUtils.CreateSpriteFromPng(mouthOpenPng),
				mouthClosed = CardUtils.CreateSpriteFromPng(mouthClosedPng),
			};
		}
	}
}