extends Control

@export var win_scene : PackedScene
@export var lose_scene : PackedScene
@onready var action_names = AppSettings.get_filtered_action_names()

var total_score : int = 0 :
	set(value):
		total_score = value
		%ScoreLabel.text = "Score: %08d" % total_score

func _ready():
	InGameMenuController.scene_tree = get_tree()

func _on_level_lost():
	InGameMenuController.open_menu(lose_scene, get_viewport())

func _on_level_won():
	$LevelLoader.advance_and_load_level()

func _on_level_loader_level_loaded():
	if $LevelLoader.current_level.has_signal("level_won"):
		$LevelLoader.current_level.level_won.connect(_on_level_won)
	if $LevelLoader.current_level.has_signal("level_lost"):
		$LevelLoader.current_level.level_lost.connect(_on_level_lost)
	if $LevelLoader.current_level.has_signal("LevelWon"):
		$LevelLoader.current_level.LevelWon.connect(_on_level_won)
	if $LevelLoader.current_level.has_signal("LevelLost"):
		$LevelLoader.current_level.LevelLost.connect(_on_level_lost)
	if $LevelLoader.current_level.has_signal("ScoreUpdated"):
		$LevelLoader.current_level.ScoreUpdated.connect(_on_level_score_updated)

func _on_level_loader_levels_finished():
	InGameMenuController.open_menu(win_scene, get_viewport())

func _on_level_score_updated(score_delta : int):
	total_score += score_delta

