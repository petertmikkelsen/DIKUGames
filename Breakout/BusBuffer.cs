using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Input;
using Breakout.Utilities;
using Breakout;
using DIKUArcade.State;

namespace Breakout {
    public class BusBuffer {
        private static BusBuffer instance;
        public List<(GameEventType, IGameEventProcessor)> subscribeBuffer {private set; get;}
        public List<(GameEventType, IGameEventProcessor)> unsubscribeBuffer {private set; get;}
        private BusBuffer() {
            subscribeBuffer = new List<(GameEventType, IGameEventProcessor)>();
            unsubscribeBuffer = new List<(GameEventType, IGameEventProcessor)>();
        }

        internal void Subscribe(GameEventType gameStateEvent)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(GameEventType type, IGameEventProcessor eventProcessor) {
            subscribeBuffer.Add((type, eventProcessor));
        }
        public void Unsubscribe(GameEventType type, IGameEventProcessor eventProcessor) {
            unsubscribeBuffer.Add((type, eventProcessor));
        }
        public static BusBuffer GetBuffer() {
            return instance ?? (instance = new BusBuffer());
        }
        public void update() {
            foreach (var subscribe in subscribeBuffer) {
                BreakoutBus.GetBus().Subscribe(subscribe.Item1, subscribe.Item2);
            }
            subscribeBuffer.Clear();
            foreach (var unsubscribe in unsubscribeBuffer) {
                BreakoutBus.GetBus().Unsubscribe(unsubscribe.Item1, unsubscribe.Item2);
            }
            unsubscribeBuffer.Clear();
        }
    }
}