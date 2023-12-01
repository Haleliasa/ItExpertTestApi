select *
from "Clients" cl
where (
	select count(1) from (
		select 1
		from "ClientContacts" cont
		where cont."ClientId" = cl."Id"
		limit 3
	)
) > 2
