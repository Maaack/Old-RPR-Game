extends Resource


class_name VirtualSet

export(Array, Resource) var virtual_quantities setget set_virtual_quantities

func _init(local_virtual_quantities=null):
	if local_virtual_quantities != null:
		set_virtual_quantities(local_virtual_quantities)

func _to_string():
	var to_string = "[Set: ["
	for virtual_quantity in virtual_quantities:
		to_string += str(virtual_quantity) + ","
	return to_string + "]]"

func set_virtual_quantities(local_virtual_quantities:Array):
	if local_virtual_quantities == null:
		return
	virtual_quantities = local_virtual_quantities

func duplicate_contents():
	var duplicate_quantities = []
	for quantity in virtual_quantities:
		duplicate_quantities.append(quantity.duplicate())
	set_virtual_quantities(duplicate_quantities)

func _append_virtual_quantity(value:VirtualQuantity):
	if value == null:
		return
	var key = value.get_group_name()
	if key == null:
		return
	virtual_quantities.append(value)
	return value

func add_virtual_quantity(value:VirtualQuantity):
	if value == null:
		return null
	var key = value.group_name
	if key == null:
		return
	for virtual_quantity in virtual_quantities:
		if virtual_quantity.get_group_name() == value.get_group_name():
			return virtual_quantity.add_virtual_quantity(value)
	return _append_virtual_quantity(value)

func add_virtual_set(value:VirtualSet):
	if value == null:
		return
	var added_quantities = duplicate()
	added_quantities.clear()
	for virtual_quantity in value.virtual_quantities:
		var added_quantity = add_virtual_quantity(virtual_quantity)
		if added_quantity != null:
			added_quantities._append_virtual_quantity(added_quantity)
	return added_quantities

func subtract_virtual_set(value:VirtualSet):
	if value == null:
		return
	var negative_quantities = value.get_negative()
	var subtracted_negative_quantities = add_virtual_set(negative_quantities)
	var subtracted_quantities = subtracted_negative_quantities.get_negative()
	return subtracted_quantities

func clear():
	virtual_quantities.clear()

func get_sum_quantity():
	var sum_quantity = 0.0
	if virtual_quantities == null:
		return sum_quantity
	for virtual_quantity in virtual_quantities:
		if virtual_quantity is VirtualQuantity:
			sum_quantity += virtual_quantity.quantity
	return sum_quantity

func get_mass():
	var sum_mass = 0.0
	if virtual_quantities == null:
		return sum_mass
	for virtual_quantity in virtual_quantities:
		if virtual_quantity is VirtualQuantity:
			sum_mass += virtual_quantity.get_mass()
	return sum_mass

func get_area():
	var sum_area = 0.0
	if virtual_quantities == null:
		return sum_area
	for virtual_quantity in virtual_quantities:
		if virtual_quantity is VirtualQuantity:
			sum_area += virtual_quantity.get_area()
	return sum_area

func get_negative():
	var negative_quantities = duplicate()
	negative_quantities.clear()
	for virtual_quantity in virtual_quantities:
		var negative_quantity = virtual_quantity.duplicate()
		negative_quantity.quantity = -(negative_quantity.quantity)
		negative_quantities._append_virtual_quantity(negative_quantity)
	return negative_quantities
