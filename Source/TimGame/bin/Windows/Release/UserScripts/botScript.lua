if(bot.yPos > 8 and bot.xPos <= 1) then

	bot:FindPathTo(1, 1);

elseif(bot.yPos < 3 and bot.xPos <= 1) then

	bot:FindPathTo(1, 8);

end
bot:FollowPath();

if(bot:IsAimedAt(1, 8)) then
	bot:Fire();
end