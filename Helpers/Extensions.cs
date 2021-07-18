﻿using static SDL2.SDL;

namespace AdLib.Helpers
{
    public static class Extensions
    {
        public static SDL_bool ToSDLBool(this bool v) => v ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE;
    }
}
