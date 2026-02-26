<script lang="ts">
	import type { CreateLovedoneRequest } from '$lib/types/types';
	import { createLovedOne } from '../../api/immfapi';

	async function addLovedOne() {
		const randNum = Math.round(Math.random() * 10000);
		const lovedOne: CreateLovedoneRequest = {
			name: 'Friend' + randNum,
			relationship: 'friend'
		};

		const createLlovedOne = createLovedOne(lovedOne);
		console.log(createLlovedOne);
	}
</script>

<button
	onclick={() => {
		addLovedOne();
	}}>Add LovedOne</button
>
