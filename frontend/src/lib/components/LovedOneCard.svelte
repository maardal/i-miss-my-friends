<script lang="ts">
	import type { LovedOne } from '$lib/types/types';
	import { createHangout } from '../../api/immfapi';
	const { loved } = $props<{ loved: LovedOne }>();
</script>

<section>
	<p>{loved.relationship}: {loved.name} + {loved.lastHangout}</p>
	<button
		onclick={() => {
			createHangout(loved.id);
		}}
		>Add hangout
	</button>
</section>
