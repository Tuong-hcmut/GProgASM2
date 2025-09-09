import pygame
import random
import sys
import os
import time
from Sprite import *  # Use Sprite, AnimatedSprite, AnimData, from_multiline_sheet
from Audio import *

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
ASSETS_DIR = os.path.join(BASE_DIR, "Assets")
CURSOR_SHEET_PATH = os.path.join(ASSETS_DIR, "UI", "cursor.png")
BACKGROUND_PATH = os.path.join(ASSETS_DIR,"Background", "background_new.png")
PLAY_BUTTON_PATH = os.path.join(ASSETS_DIR,"Play_button.png")
SETTINGS_BUTTON_PATH = os.path.join(ASSETS_DIR,"Settings_button.png")
EXIT_BUTTON_PATH = os.path.join(ASSETS_DIR,"Exit.png")

BASE_WIDTH = 3200
BASE_HEIGHT = 1792
WINDOW_WIDTH = 1200
WINDOW_HEIGHT = 800
SPRITE_HEIGHT = 120
FPS = 60
NUM_GRAVES = 9

base_resolution = (BASE_WIDTH,BASE_HEIGHT)
window_resolution = (WINDOW_WIDTH,WINDOW_HEIGHT)
sx = WINDOW_WIDTH / BASE_WIDTH
sy = WINDOW_HEIGHT / BASE_HEIGHT

pygame.init()
clock = pygame.time.Clock()

# ===== Sprite subclasses =====

class Cursor(Sprite):
    def __init__(self, x=0, y=0, target_height= SPRITE_HEIGHT, base_res=base_resolution, curr_res=window_resolution):
        image = pygame.image.load(CURSOR_SHEET_PATH).convert_alpha()
        super().__init__(image, x, y, target_height, base_resolution=base_res, current_resolution=curr_res)

    def draw_centered(self, surface):
        mouse_x, mouse_y = pygame.mouse.get_pos()
        rect = self.image.get_rect(center=(mouse_x, mouse_y))
        surface.blit(self.image, rect.topleft)

class Button(Sprite):
    def __init__(self, image_path, x, y, target_height=SPRITE_HEIGHT, base_res=base_resolution, curr_res=window_resolution):
        image = pygame.image.load(image_path).convert_alpha()
        super().__init__(image, x, y, target_height, base_resolution=base_res, current_resolution=curr_res)

    def is_clicked(self, event):
        if event.type == pygame.MOUSEBUTTONDOWN and event.button == 1:
            if self.rect.collidepoint(event.pos):
                return True
        return False

# ===== Debugger =====
class Debugger:
    def __init__(self, mode_arg):
        self.mode = mode_arg
    def log(self, message):
        if self.mode == "debug":
            print("Debugger log: " + str(message))


# ===== Game =====
class Game:
    def __init__(self):
        self.window = pygame.display.set_mode(window_resolution)
        pygame.display.set_caption("Whack-a-zombie")
        self.debugger = Debugger("debug")
        self.audio = Audio(window_resolution)

        # Cursor
        self.cursor = Cursor(curr_res=window_resolution)
          
    # ===== Volume bar =====
    def handle_volume_event(self, event):
        self.audio.music_bar.handle_event(event)
        self.audio.hit_bar.handle_event(event)

    def draw_volume_bar(self):
        self.audio.music_bar.draw(self.window)
        self.audio.hit_bar.draw(self.window)

    # ========= DRAW HUD ==========

    def draw_hud(self):
        font = pygame.font.SysFont("Arial",24,bold=True)
        accuracy = 0
        total = self.hits + self.misses
        if total > 0:
            accuracy = int(self.hits/total*100)
        text = f"Hits: {self.hits}  Misses: {self.misses}  Accuracy: {accuracy}%"
        label = font.render(text, True, (255,255,255))
        self.window.blit(label, (10,10))

    # ===== Main menu =====
    def main_menu(self):
        pygame.mouse.set_visible(False)
        clock = pygame.time.Clock()
        menu_bg = pygame.image.load(BACKGROUND_PATH).convert_alpha()
        menu_bg = pygame.transform.scale(menu_bg, window_resolution)

        # Initialize buttons
        start_button = Button(PLAY_BUTTON_PATH, 0, 650, curr_res=window_resolution)
        settings_button = Button(SETTINGS_BUTTON_PATH, 0, 910, curr_res=window_resolution)
        exit_button = Button(EXIT_BUTTON_PATH, 0, 1170, curr_res=window_resolution)

        BUTTON_HEIGHT = int(SPRITE_HEIGHT * 1.2)  # adjust multiplier to tune size
        start_y = 650
        spacing = BUTTON_HEIGHT + 40
        start_button = Button(PLAY_BUTTON_PATH, 0, start_y, target_height=BUTTON_HEIGHT, curr_res=window_resolution)
        settings_button = Button(SETTINGS_BUTTON_PATH, 0, start_y + spacing, target_height=BUTTON_HEIGHT, curr_res=window_resolution)
        exit_button = Button(EXIT_BUTTON_PATH, 0, start_y + spacing*2, target_height=BUTTON_HEIGHT, curr_res=window_resolution)
        
        # Center x based on scaled image width
        for btn in [start_button, settings_button, exit_button]:
            btn.x = WINDOW_WIDTH//2 - btn.rect.width//2


        in_menu = True
        in_settings = False
        title_font = pygame.font.SysFont("Arial", 80, bold=True)

        while in_menu:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    pygame.quit()
                    sys.exit()

                if not in_settings:
                    if start_button.is_clicked(event):
                        return "start"
                    elif settings_button.is_clicked(event):
                        in_settings = True
                    elif exit_button.is_clicked(event):
                        pygame.quit()
                        sys.exit()
                else:
                    self.handle_volume_event(event)
                    if event.type == pygame.KEYDOWN and event.key == pygame.K_ESCAPE:
                        in_settings = False

            # Draw background
            self.window.blit(menu_bg, (0,0))

            # Draw title
            text = "Whack A Zombie" if not in_settings else "Settings"
            creepy_color = (180,0,0)
            title_label = title_font.render(text, True, creepy_color)
            outline = title_font.render(text, True, (0,0,0))
            x = WINDOW_WIDTH//2 - title_label.get_width()//2
            y = 120
            for dx in [-3,-2,-1,0,1,2,3]:
                for dy in [-3,-2,-1,0,1,2,3]:
                    if dx!=0 or dy!=0:
                        self.window.blit(outline, (x+dx, y+dy))
            self.window.blit(title_label, (x, y))

            if not in_settings:
                start_button.draw(self.window)
                settings_button.draw(self.window)
                exit_button.draw(self.window)
            else:
                self.draw_volume_bar()
                small_font = pygame.font.SysFont("Arial", 20)
                esc_label = small_font.render("Press ESC to return", True, (200,200,200))
                self.window.blit(esc_label, (WINDOW_WIDTH//2-90, 500))

            # Draw cursor
            self.cursor.draw_centered(self.window)


            pygame.display.flip()
            clock.tick(60)

    # ===== Game start =====
    def start(self):
        self.score = 0
        spawn_timer = 0

        self.background = pygame.image.load(BACKGROUND_PATH).convert_alpha()
        self.background = pygame.transform.scale(self.background, window_resolution)

        running = True
        while running:
            dt = clock.tick(FPS)/1000.0
            spawn_timer += dt
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    running = False
                    pygame.quit()
                    sys.exit()
                elif event.type == pygame.KEYDOWN and event.key == pygame.K_ESCAPE:
                    running = False
                    return "menu"
                self.handle_volume_event(event)

                if event.type == pygame.MOUSEBUTTONDOWN and event.button == 1:

                    clicked = False
                    # Swing hammer
                    self.hammer.swing()

                    if not clicked:
                        self.misses += 1

            self.window.blit(self.background,(0,0))

            self.draw_volume_bar()

            self.cursor.draw_centered(self.window)

            self.draw_hud()

            self.draw_hammer()

            pygame.display.flip()


# ===== Main loop =====
game_instance = Game()
try:
    while True:
        choice = game_instance.main_menu()
        if choice == "start":
            result = game_instance.start()
            if result == "menu":
                continue
        else:
            break
finally:
    pygame.quit()
    sys.exit()
