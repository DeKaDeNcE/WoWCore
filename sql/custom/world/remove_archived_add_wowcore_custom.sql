DELETE FROM `updates` WHERE `state` = 'ARCHIVED';
DELETE FROM `updates_include` WHERE `state` = 'ARCHIVED';
DELETE FROM `updates_include` WHERE `path` = '$/sql/wowcore/world';
INSERT INTO `updates_include`
(               `path`,    `state`) VALUES
('$/sql/wowcore/world', 'RELEASED');