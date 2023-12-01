with "DatesOrdered" as (
	select
		*,
		row_number() over(order by "Id", "Dt") as "Num"
	from "Dates"
)

select
	cur."Id",
	cur."Dt" as "Sd",
	nex."Dt" as "Ed"
from "DatesOrdered" cur
join "DatesOrdered" nex on nex."Id" = cur."Id" and nex."Num" = (cur."Num" + 1)
