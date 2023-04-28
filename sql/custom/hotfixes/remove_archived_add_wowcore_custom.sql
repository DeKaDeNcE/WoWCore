DELETE FROM `updates` WHERE `state` = 'ARCHIVED';
DELETE FROM `updates_include` WHERE `state` = 'ARCHIVED';
DELETE FROM `updates_include` WHERE `path` = '$/sql/wowcore/hotfixes';
INSERT INTO `updates_include`
(                  `path`,    `state`) VALUES
('$/sql/wowcore/hotfixes', 'RELEASED');