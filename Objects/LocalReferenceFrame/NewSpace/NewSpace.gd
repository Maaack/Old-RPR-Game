extends "res://Objects/WorldSpace/WorldSpace.gd"


export(Resource) var virtual_space setget set_virtual_space

func set_virtual_space(local_virtual_space:VirtualSpace):
	if local_virtual_space == null:
		return
	virtual_space = local_virtual_space

func _physics_process(delta):
	if virtual_space == null:
		return
	if virtual_space.has_method("physics_process"):
		virtual_space.physics_process(delta)

func spawn(physical_unit:PackedScenesUnit):
	if physical_unit == null:
		print("Error: Spawn rigid body called with null!")
		return
	var instance = physical_unit.world_space_scene.instance()
	add_child(instance)
	instance.add_to_group(physical_unit.group_name)
	instance.physical_unit = physical_unit
	return instance
