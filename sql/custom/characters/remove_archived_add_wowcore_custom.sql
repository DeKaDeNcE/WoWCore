DELETE FROM `updates` WHERE `state` = 'ARCHIVED';
DELETE FROM `updates_include` WHERE `state` = 'ARCHIVED';
DELETE FROM `updates_include` WHERE `path` = '$/sql/wowcore/characters';
INSERT INTO `updates_include`
(                    `path`,    `state`) VALUES
('$/sql/wowcore/characters', 'RELEASED');