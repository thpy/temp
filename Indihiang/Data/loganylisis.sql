/*�d���`���έp*/
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.a_day = 13 group by page_access )
	order by  avgtime desc;
/*�d�ߦphum�t�Ϊ��X�ݲέp*/
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hum%' group by page_access )
	order by  avgtime desc;
/*�w��Y�@�����X�ݩ��Ӭd��*/	
select a.* from log_data a where a.page_access = '/hum/html/EveryDay_work-1.html'  and a.a_day = 11 order by time_taken desc;
/*�d�ߦphr_sys�t�Ϊ��X�ݲέp*/
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hr_sys%'   group by page_access )
	order by  avgtime desc;
---------------------------------	
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hum%'  and t.a_day = 23  group by page_access )   --and t.page_access like '%every%html' 
	order by  avgtime desc;
	
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hr_sys%'   and t.a_day = 11 group by page_access )
	order by  avgtime desc;
	
select a.* from log_data a where a.page_access like '%SupportHuman%';--�H�O�䴩
select a.* from log_data a where a.page_access like '%hum%'; --�H�O�X��
select a.* from log_data a where a.page_access like '%menjin%'; --�s�t��^���޲z

select a.a_time from log_data a where a.page_access = '/hum/ashx/Angular_Work/EveryDay_workaffirmlog.ashx'  and a.a_day = '23' group by a.a_time;
select a.* from log_data a where a.page_access = '/hum/ashx/Angular_Work/EveryDay_workaffirmlog.ashx'  and a.a_day = '23'  order by  a.time_taken desc;