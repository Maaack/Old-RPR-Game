extends VirtualUnit


class_name VirtualQuantity

enum NumericalUnitSetting{ CONTINUOUS, DISCRETE }
@export var numerical_unit: NumericalUnitSetting
@export var quantity: float = 1.0: set = set_quantity

func _to_string():
	return "[Quantity: [" + super._to_string() + ", " + str(quantity) + "]]"

func set_quantity(value:float):
	if value == null:
		return
	quantity = value
	if numerical_unit == NumericalUnitSetting.DISCRETE:
		quantity = floor(quantity)

func add_quantity(value:float):
	if value == null or value == 0.0:
		return
	set_quantity(quantity + value)

func split(value:float):
	if value == null:
		return
	var split_quantity = duplicate()
	value = min(value, quantity)
	add_quantity(-value)
	split_quantity.quantity = value
	return split_quantity

func get_virtual_unit():
	var virtual_unit = duplicate()
	virtual_unit.quantity = 1.0
	return virtual_unit

func add_virtual_quantity(value:VirtualQuantity):
	if value == null:
		return
	if value.get_group_name() != get_group_name():
		print("Error: Adding incompatible quantities ", str(value), " and ", str(self))
		return
	add_quantity(value.quantity)	

func get_mass():
	return quantity * mass

func get_unit_mass():
	return mass

func get_unit_area():
	return super.get_area()

func get_area():
	return quantity * super.get_area()

func get_quantity_for_area(value:float):
	var unit_area = super.get_area()
	return value / unit_area
